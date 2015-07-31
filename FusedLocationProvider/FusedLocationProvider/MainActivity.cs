using System;
using Android.App;
using Android.OS;
using Android.Gms.Location;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Util;
using Android.Widget;
using Android.Locations;
using Android.Hardware;
using System.Text;
using Android.Runtime;
using FusedLocationProvider.Lib;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Globalization;
using FusedLocationProvider.Xml2CSharp;
using System.IO;
using System.Xml.Serialization;

namespace FusedLocationProvider
{
	[Activity (Label = "FusedLocationProvider", MainLauncher = true)]
	public class MainActivity : Activity, IGoogleApiClientConnectionCallbacks,
	IGoogleApiClientOnConnectionFailedListener, Android.Gms.Location.ILocationListener, ISensorEventListener
    {
        private static readonly object _syncLock = new object();
        IGoogleApiClient apiClient;
		LocationRequest locRequest;
		Button button;
		TextView latitude;
		TextView longitude;
		TextView provider;
		Button button2;
		TextView latitude2;
		TextView longitude2;
		TextView provider2;
        TextView txtRaw;
        TextView txtOverallDev;
        TextView txtStdDev;
        TextView txtCondition;

        EditText txtRange1;
        EditText txtRange2;
        EditText txtRange3;
        EditText txtRange4;
        Button btnGenerateKML;
        ToggleButton togglebutton;

        private ListView mlistView;
        ListViewAdapter _arrAdp;
        int _totalSamples = 0;
        


        System.Timers.Timer _tmrOverallAvg = new System.Timers.Timer();
        System.Timers.Timer _tmrSampling = new System.Timers.Timer();


        private SensorManager mSensorManager;
        private Sensor mOrientation;

        bool _isGooglePlayServicesInstalled;


        //Application
        private DataProcessor _dataProcessor = null;
        private const int SAMPLE_COUNT= 10;
        private const int SAMPLE_TIME_INTERVAL = 1000;
        private bool _continueSampling = true;
        private int _currentSampleCount = 0;
        private List<SensorData> _sensorData = null;
        private GPXDataSet _gpxDataSet = null;
        private KMLGenerator _kmlGen = null;
        private int fileID = 0;

        ////Lifecycle methods


        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Log.Debug ("OnCreate", "OnCreate called, initializing views...");

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// UI to print last location
			button = FindViewById<Button> (Resource.Id.myButton);
			latitude = FindViewById<TextView> (Resource.Id.latitude);
			longitude = FindViewById<TextView> (Resource.Id.longitude);
			provider = FindViewById<TextView> (Resource.Id.provider);

			// UI to print location updates
			button2 = FindViewById<Button> (Resource.Id.myButton2);
			latitude2 = FindViewById<TextView> (Resource.Id.latitude2);
			longitude2 = FindViewById<TextView> (Resource.Id.longitude2);
			provider2 = FindViewById<TextView> (Resource.Id.provider2);

            //UI Sensor
            txtRaw = FindViewById<TextView>(Resource.Id.txtRaw);
            txtStdDev = FindViewById<TextView>(Resource.Id.txtStdDev);
            txtOverallDev = FindViewById<TextView>(Resource.Id.txtAvgStdDev);
            txtCondition = FindViewById<TextView>(Resource.Id.txtCondition);

            txtRange1 = FindViewById<EditText>(Resource.Id.txtRange1);
            txtRange2 = FindViewById<EditText>(Resource.Id.txtRange2);
            txtRange3 = FindViewById<EditText>(Resource.Id.txtRange3);
            txtRange4 = FindViewById<EditText>(Resource.Id.txtRange4);
            //txtRange1.Text = "0.45";
            //txtRange2.Text = "0.75";
            //txtRange3.Text = "1.2";
            //txtRange4.Text = "3";
            txtRange1.Text = "0.8"; 
            txtRange2.Text = "1.0";
            txtRange3.Text = "2.0";
            txtRange4.Text = "5.0";

            _kmlGen = new KMLGenerator();
            btnGenerateKML = FindViewById<Button>(Resource.Id.btnGenerateKML);
            togglebutton = FindViewById<ToggleButton>(Resource.Id.togglebutton);

            togglebutton.Checked = true;
            
            mSensorManager = (SensorManager)GetSystemService(SensorService);
            _gpxDataSet = new GPXDataSet();
            _sensorData = new List<SensorData>();
            _tmrOverallAvg.Elapsed += new ElapsedEventHandler(_gpxDataSet.OnOverallAvgTimedEvent);
            _tmrOverallAvg.Interval = 5000;
            //_tmrOverallAvg.Enabled = true;

            _tmrSampling.Elapsed += new ElapsedEventHandler(this.OnSamplingTimedEvent);
            _tmrSampling.Interval = SAMPLE_TIME_INTERVAL;
            //_tmrSampling.Enabled = true;

            _tmrSampling.Enabled = false;
            _tmrOverallAvg.Enabled = false;

            mlistView = FindViewById<ListView>(Resource.Id.myListView);
            _arrAdp = new ListViewAdapter(this, _gpxDataSet.GPXDataList);
            mlistView.Adapter = _arrAdp;


            _isGooglePlayServicesInstalled = IsGooglePlayServicesInstalled ();

			if (_isGooglePlayServicesInstalled) {
				// pass in the Context, ConnectionListener and ConnectionFailedListener
				apiClient = new GoogleApiClientBuilder (this, this, this)
					.AddApi (LocationServices.API).Build ();

				// generate a location request that we will pass into a call for location updates
				locRequest = new LocationRequest ();

			} else {
				Log.Error ("OnCreate", "Google Play Services is not installed");
				Toast.MakeText (this, "Google Play Services is not installed", ToastLength.Long).Show ();
				Finish ();
			}

		}

    

		bool IsGooglePlayServicesInstalled()
		{
			int queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable (this);
			if (queryResult == ConnectionResult.Success)
			{
				Log.Info ("MainActivity", "Google Play Services is installed on this device.");
				return true;
			}

			if (GoogleApiAvailability.Instance.IsUserResolvableError (queryResult))
			{
				string errorString = GoogleApiAvailability.Instance.GetErrorString (queryResult);
				Log.Error ("ManActivity", "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);

				// Show error dialog to let user debug google play services
			}
			return false;
		}

		protected override void OnResume()
		{
			base.OnResume ();
			Log.Debug ("OnResume", "OnResume called, connecting to client...");
            mSensorManager.RegisterListener(this, mSensorManager.GetDefaultSensor(SensorType.LinearAcceleration), SensorDelay.Ui);

            apiClient.Connect();
            togglebutton.RequestFocus();
            togglebutton.Click += (o, e) => {
                // Perform action on clicks
                if (togglebutton.Checked)
                    Toast.MakeText(this, "Checked", ToastLength.Short).Show();
                else
                    Toast.MakeText(this, "Not checked", ToastLength.Short).Show();
            };

            // Clicking the first button will make a one-time call to get the user's last location
            button.Click += delegate {
				if (apiClient.IsConnected)
				{
					button.Text = "Getting Last Location";

					Location location = LocationServices.FusedLocationApi.GetLastLocation (apiClient);
					if (location != null)
					{
						latitude.Text = "Latitude: " + location.Latitude.ToString();
						longitude.Text = "Longitude: " + location.Longitude.ToString();
						provider.Text = "Provider: " + location.Provider.ToString();
						Log.Debug ("LocationClient", "Last location printed");
					}
				}
				else
				{
					Log.Info ("LocationClient", "Please wait for client to connect");
				}
			};

			// Clicking the second button will send a request for continuous updates
			button2.Click += delegate {
				if (apiClient.IsConnected)
				{
					button2.Text = "Requesting Location Updates";

					// Setting location priority to PRIORITY_HIGH_ACCURACY (100)
					locRequest.SetPriority(100);

					// Setting interval between updates, in milliseconds
					// NOTE: the default FastestInterval is 1 minute. If you want to receive location updates more than 
					// once a minute, you _must_ also change the FastestInterval to be less than or equal to your Interval
					locRequest.SetFastestInterval(500);
					locRequest.SetInterval(1000);

					Log.Debug("LocationRequest", "Request priority set to status code {0}, interval set to {1} ms", 
						locRequest.Priority.ToString(), locRequest.Interval.ToString());

					// pass in a location request and LocationListener
					LocationServices.FusedLocationApi.RequestLocationUpdates (apiClient, locRequest, this);
					// In OnLocationChanged (below), we will make calls to update the UI
					// with the new location data
				}
				else
				{
					Log.Info("LocationClient", "Please wait for Client to connect");
				}
			};

            btnGenerateKML.Click += delegate {
                //ThreadPool.QueueUserWorkItem(o => CreateKMLFile());
                ThreadPool.QueueUserWorkItem(o => CreateCSVFile());
            };


        }

        private void CreateKMLFile()
        {
            fileID++;
            if (_gpxDataSet.Segments.Count == 0) return;
            Document d = KMLGenerator.GenerateKML(_gpxDataSet.Segments);
            //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            string filename = Path.Combine(path  , DateTime.Now.ToLongTimeString() + "_test.txt");
           
            using (TextWriter writer = new StreamWriter(filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Document));
                serializer.Serialize(writer, d);
            }
           
            _gpxDataSet.Segments.Clear();
        }

        private void CreateCSVFile()
        {
            if (_gpxDataSet.Segments.Count == 0) return;
            
            
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            
            string filenameCSV = Path.Combine(path , "EXCEL_" + DateTime.Now.ToLongTimeString() + "_data.csv");
            
            using (TextWriter writer = new StreamWriter(filenameCSV))
            {
                StringBuilder sb = new StringBuilder();
                writer.Write(KMLGenerator.GenerateCSV(_gpxDataSet.Segments));
                writer.Close();
            }

        }

		protected override void OnPause ()
		{
			base.OnPause ();
			Log.Debug ("OnPause", "OnPause called, stopping location updates");
            mSensorManager.UnregisterListener(this);

            if (apiClient.IsConnected) {
				// stop location updates, passing in the LocationListener
				LocationServices.FusedLocationApi.RemoveLocationUpdates (apiClient, this);

				apiClient.Disconnect ();
			}
		}


		////Interface methods

		public void OnConnected (Bundle bundle)
		{
			// This method is called when we connect to the LocationClient. We can start location updated directly form
			// here if desired, or we can do it in a lifecycle method, as shown above 

			// You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
			Log.Info("LocationClient", "Now connected to client");
		}

		public void OnDisconnected ()
		{
			// This method is called when we disconnect from the LocationClient.

			// You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
			Log.Info("LocationClient", "Now disconnected from client");
		}

		public void OnConnectionFailed (ConnectionResult bundle)
		{
			// This method is used to handle connection issues with the Google Play Services Client (LocationClient). 
			// You can check if the connection has a resolution (bundle.HasResolution) and attempt to resolve it

			// You must implement this to implement the IGooglePlayServicesClientOnConnectionFailedListener Interface
			Log.Info("LocationClient", "Connection failed, attempting to reach google play services");
		}

		public void OnLocationChanged (Location location)
		{
			// This method returns changes in the user's location if they've been requested
			 
			// You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
			Log.Debug ("LocationClient", "Location updated");

			latitude2.Text = "Latitude: " + location.Latitude.ToString();
            longitude2.Text = "Longitude: " + location.Longitude.ToString() + "SPEED : " + location.Speed.ToString();
			provider2.Text = "Provider: " + location.Provider.ToString();
            int startVal = -1;
            if (togglebutton.Checked)
                startVal = 3;

            if (location.Speed > startVal)
            {
                if (!_tmrSampling.Enabled)
                {
                    _tmrSampling.Enabled = true;
                    _tmrOverallAvg.Enabled = true;
                    _continueSampling = true;
                }
               
            }
            else if (location.Speed < 1)
            {
                _tmrSampling.Enabled = false;
                _tmrOverallAvg.Enabled = false;
                _continueSampling = false;
                ThreadPool.QueueUserWorkItem(o => CreateKMLFile());
                               
            }
           
		}

		public void OnConnectionSuspended (int i)
		{
			
		}

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                var text = new StringBuilder("YAW = ")
                    .Append(e.Values[0])
                    .Append(", ROLL=")
                    .Append(e.Values[1])
                    .Append(", PITCH=")
                    .Append(e.Values[2]);
                txtRaw.Text = text.ToString();
                //mSensorTextView.Text = text.ToString();
               

                if (_continueSampling)
                {
                   
                    Location loca = LocationServices.FusedLocationApi.GetLastLocation(apiClient);
                    _sensorData.Add(new SensorData()
                    {
                        Lat = loca.Latitude,
                        Log = loca.Longitude,
                        Speed = loca.Speed,
                        Yaw = e.Values[0],
                        Roll = e.Values[1],
                        Pitch = e.Values[2],
                        Time = DateTime.Now//TODO: later get it in constr
                    });
                    //txtCondition.Text = _currentSampleCount.ToString();
                }
                else
                {

                    if (_sensorData.Count == 0) return;
                    ThreadPool.QueueUserWorkItem(o => ProcessData());
                    
                }
            }
        }

        public void OnSamplingTimedEvent(object source, ElapsedEventArgs e)
        {
            _continueSampling = false;
        }

        public void ProcessData()
        {
            _currentSampleCount++;
            _dataProcessor = new DataProcessor(_sensorData);
            float r1 = 0 , r2 = 0, r3 = 0,r4=0;
            float.TryParse(txtRange1.Text, out r1);
            float.TryParse(txtRange2.Text, out r2);
            float.TryParse(txtRange3.Text, out r3);
            float.TryParse(txtRange4.Text, out r4);
            _dataProcessor.Range1 = r1;
            _dataProcessor.Range2 = r2;
            _dataProcessor.Range3 = r3;
            _dataProcessor.Range4 = r4;

            GPXData gpxDT = _dataProcessor.ProcessDataForGPX();
            RunOnUiThread(() => txtStdDev.Text = gpxDT.ToString() + "\n" + _currentSampleCount.ToString());
            RunOnUiThread(() => txtOverallDev.Text = _gpxDataSet.ToString());
            RunOnUiThread(() => txtCondition.Text = gpxDT.RoadCondition.ToString());

            
            _gpxDataSet.AddGPXData(gpxDT);
            
            _sensorData = new List<SensorData>();
            _continueSampling = true;
            //RunOnUiThread(() => _arrAdp.NotifyDataSetChanged());

        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum]SensorStatus accuracy)
        {
            //throw new NotImplementedException();
        }
    }
}


