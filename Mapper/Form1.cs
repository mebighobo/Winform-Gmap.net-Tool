using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using System.Diagnostics;

namespace Mapper
{
    public partial class Pharmacy : Form
    {

        GMapOverlay pharmOverlay = new GMapOverlay("Pharm");
        GMapOverlay routes = new GMapOverlay("routes");

        Dictionary<string, GMapMarker> listMarkers = new Dictionary<string, GMapMarker>();
        List<GMapRoute> mapRoutes = new List<GMapRoute>();

        Dictionary<string, PointLatLng> locations = new Dictionary<string, PointLatLng>();

        List<ListViewItem> listViewItems = new List<ListViewItem>();

        GMapMarker currentMarker;
        GMapMarker marker1;
        GMapRoute route;

        public Pharmacy()
        {
            InitializeComponent();
            initListView();
        }

        private void MainMap_Load(object sender, EventArgs e)
        {
            MainMap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            MainMap.SetPositionByKeywords("Vancouver, Canada");
            MainMap.ShowCenter = false;
            MainMap.Overlays.Add(pharmOverlay);
            MainMap.Overlays.Add(routes);
            addLocations();

            var bs = new BindingSource(locations, null);

            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";

            MainMap.MouseClick += new MouseEventHandler(map_MouseClick);
        }

        private void initListView()
        {
            // Add ListView columns
            listView1.View = View.Details;
            listView1.Columns.Add("Location", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Distance(Km)", 200, HorizontalAlignment.Left);
        }
        public void addLocations()
        {
            //Add location
            locations.Add("ALBERTO PHARMACY # 1", new PointLatLng(49.2619707, -123.069488));
            locations.Add("ALBERTO PHARMACY NO. 2", new PointLatLng(49.2607233, -123.0695212));
            locations.Add("BENTALL PHARMACY", new PointLatLng(49.2864943, -123.12143630000003));
            locations.Add("BIOPRO BIOLOGICS PHARMACY", new PointLatLng(49.2635473, -123.12293629999999));
            locations.Add("BOND STREET PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            locations.Add("BOOMER DRUGS", new PointLatLng(49.208856, -123.140283));
            locations.Add("BROADWAY PHARMASAVE #73", new PointLatLng(49.264082, -123.151204));
            locations.Add("CAMBIE PHARMASAVE", new PointLatLng(49.249580, -123.115192));
            locations.Add("CANPHARM DRUGS", new PointLatLng(49.231110, -123.065809));
            locations.Add("CAREVILLE PHARMACY UBC", new PointLatLng(49.254691, -123.235713));
            locations.Add("CLOUD IPHARMACY INC.", new PointLatLng(49.239514, -123.065542));
            locations.Add("COAL HARBOUR PHARMACY", new PointLatLng(49.287595, -123.124223));
            locations.Add("CONTINENTAL PHARMACY #2", new PointLatLng(49.236665, -123.065088));
            locations.Add("CORNING DRUGS #2", new PointLatLng(49.278414, -123.098542));

            //locations.Add("CORNING DRUGS LTD.", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("COSTCO PHARMACY # 552", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("DAVIE PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("DOWNTOWN CLINIC PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("DTES CONNECTIONS PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("DUNDAS REMEDY'S RX PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("EAST END PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("EASTSIDE PHARMACY LTD.", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("EVERWELL PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("FINLANDIA NATURAL PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("FRASER NEIGHBOURHOOD PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("FRASER OUTREACH PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("FRASER PHARMACHOICE", new PointLatLng(49.2088562, -123.14028289999999));

            //locations.Add("FRASER PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("GARLANE PHARMACY #1", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("GARLANE PHARMACY #2", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("GARLANE PRESCRIPTIONS", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("HARVARD PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("HEALTHSIDE PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("JEFF'S PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("JERICHO PHARMACY & HEALTH FOOD STORE", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("KERRISDALE MEDICINE CENTRE PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("KINGSWAY PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("KRIPPS HEALTHCARE RX", new PointLatLng(49.2088562, -123.14028289999999));

            //locations.Add("LANCASTER MEDICAL SUPPL. & PRESC. #1", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LAUREL PRESCRIPTIONS", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LG PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LITTLE MOUNTAIN PHARMACY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LOBLAW PHARMACY #1517", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LOBLAW PHARMACY #1520", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LOBLAW PHARMACY #4617", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LOBLAW PHARMACY #4979", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LONDON DRUGS # 2", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LONDON DRUGS # 4 - BROADWAY", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LONDON DRUGS # 7 - HASTINGS", new PointLatLng(49.2088562, -123.14028289999999));
            //locations.Add("LONDON DRUGS #10", new PointLatLng(49.2088562, -123.14028289999999));
        }

        //set current point
        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                double lat = MainMap.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = MainMap.FromLocalToLatLng(e.X, e.Y).Lng;

                if (!pharmOverlay.Markers.Any(i => i.Tag == "1"))
                {
                    currentMarker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.arrow);
                    currentMarker.Tag = "1";
                    pharmOverlay.Markers.Add(currentMarker);
                }
                else
                {
                    pharmOverlay.Markers.Remove(currentMarker);
                    currentMarker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.arrow);
                    currentMarker.Tag = "1";
                    pharmOverlay.Markers.Add(currentMarker);
                }
            }

            // debuging purpose
            List<Placemark> plc = null;
            var st = GMapProviders.GoogleMap.GetPlacemarks(MainMap.FromLocalToLatLng(e.X, e.Y), out plc);
            if (st == GeoCoderStatusCode.G_GEO_SUCCESS && plc != null)
            {
                foreach (var pl in plc)
                {
                    if (!string.IsNullOrEmpty(pl.PostalCodeNumber))
                    {
                        Debug.WriteLine("Accuracy: " + pl.Accuracy + ", " + pl.Address + ", PostalCodeNumber: " + pl.PostalCodeNumber);
                    }
                }
            }
        }

        public string checkString(string name)
        {
            int index = name.IndexOf(',');
            if (index > 0)
            {
                return name.Substring(0, index) + "]";
            }
            return name;
        }

        //Add to listbox
        private void button1_Click(object sender, EventArgs e)
        {
            string temp = comboBox1.SelectedItem.ToString();
            var name = checkString(temp);

            if (!(pharmOverlay.Markers.Select(x => x.Tag).Contains(name)))
            {
                PointLatLng comboValue = (PointLatLng)comboBox1.SelectedValue;
                marker1 = new GMarkerGoogle(comboValue, GMarkerGoogleType.blue_pushpin);
                marker1.Tag = name;
                listBox1.Items.Add(name);
                pharmOverlay.Markers.Add(marker1);
                listMarkers.Add(name, marker1);
            }
            else
            {
                MessageBox.Show("The maker has already been selected. Please remove it from the list");
            }
        }

        //Get Route and Distance
        private void button16_Click(object sender, EventArgs e)
        {
            if (currentMarker == null)
            {
                MessageBox.Show("Please set an orgin marker");
            }
            if (listMarkers.Count == 0 && currentMarker != null)
            {
                MessageBox.Show("Please add a marker");
            }
            if (listMarkers.Count > 0 && currentMarker != null)
            {

                routes.Clear();
                MainMap.ReloadMap();

                Dictionary<PointLatLng, string> points = new Dictionary<PointLatLng, string>();

                foreach (var ml in listMarkers)
                {
                    points.Add(ml.Value.Position, ml.Key);
                }

                splitPoint(points, ref mapRoutes);


                foreach (var r in mapRoutes)
                {

                    r.Stroke = new Pen(Color.Red, 3);
                    routes.Routes.Add(r);

                    if (!listViewItems.Any(x => x.Text == r.Tag))
                    {
                        listViewItems.Add(new ListViewItem(new[] { r.Tag.ToString(), r.Distance.ToString().Substring(0, 4) + " Km" }));
                    }
                }

                listView1.Items.Clear();

                foreach (var ii in listViewItems)
                {
                    listView1.Items.Add(ii);
                }
            }

        }

        public void splitPoint(Dictionary<PointLatLng, string> p, ref List<GMapRoute> g)
        {
            Dictionary<string, PointLatLng> ppp = new Dictionary<string, PointLatLng>();
            int i = 0;
            foreach (var pp in p)
            {
                ppp.Add(pp.Value, pp.Key);
                ppp.Add(i.ToString(), currentMarker.Position);
                route = new GMapRoute(ppp.Values, "");
                route.Tag = pp.Value;

                if (!g.Any(x => x.Tag == route.Tag))
                {
                    g.Add(route);
                }
                i++;
            }
        }

        //Clear All Map
        private void button17_Click(object sender, EventArgs e)
        {
            if (pharmOverlay.Markers.Count > 0)
            {
                pharmOverlay.Clear();
                listMarkers.Clear();
                listBox1.Items.Clear();

            }
            if (routes.Routes.Count > 0)
            {
                routes.Clear();
                mapRoutes.Clear();
                listView1.Clear();
            }
            MainMap.ReloadMap();
        }

        //Remove 
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if ((pharmOverlay.Markers.Select(x => x.Tag).Contains(listBox1.SelectedItem)))
                {

                    mapRoutes.Remove(routes.Routes.Where(x => x.Tag == listBox1.SelectedItem).FirstOrDefault());
                    routes.Routes.Remove(routes.Routes.Where(x => x.Tag == listBox1.SelectedItem).FirstOrDefault());

                    var item = listMarkers.First(x => x.Value.Tag == listBox1.SelectedItem);
                    pharmOverlay.Markers.Remove(pharmOverlay.Markers.Where(x => x.Tag == listBox1.SelectedItem).FirstOrDefault());
                    listMarkers.Remove(item.Key);

                    listBox1.Items.Remove(listBox1.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select a marker to remove.");
            }
        }
    }
}
