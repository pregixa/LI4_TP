using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using Android.Views.InputMethods;

namespace Mobile
{
    [Activity(Label = "Grava��o", Icon = "@android:color/transparent")]

    public class GravaVoz : Activity
    {

        private MediaRecorder _recorder;
        private LinearLayout mLinearLayout;
        private Button _start;
        private Button _stop;
        private string id_vis;
        private EditText _desc_voz;
        private byte[] voice_data;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.GravaVoz);

            mLinearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayoutVoz);
            _desc_voz = FindViewById<EditText>(Resource.Id.desc_voz);
            _start = FindViewById<Button>(Resource.Id.start);
            _stop = FindViewById<Button>(Resource.Id.stop);
            id_vis = Intent.GetStringExtra("visita");

            mLinearLayout.Click += MLinearLayout_Click;

            string path = "/sdcard/test.3gpp";

            _start.Click += delegate {
                _stop.Enabled = !_stop.Enabled;
                _start.Enabled = !_start.Enabled;

                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                _recorder.SetAudioEncoder(AudioEncoder.AmrNb);
                _recorder.SetOutputFile(path);
                _recorder.Prepare();
                _recorder.Start();
            };

            _stop.Click += delegate {
                _stop.Enabled = !_stop.Enabled;
                _start.Enabled = !_start.Enabled;

                _recorder.Stop();
                _recorder.Reset();
                voice_data = File.ReadAllBytes(path);

                if (voice_data != null)
                {
                    WebClient client = new WebClient();
                    Uri uri = new Uri("http://192.168.1.69:8080/InsertVoice.php");

                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("Descricao", _desc_voz.Text);
                    parameters.Add("Voz", Convert.ToBase64String(voice_data));
                    parameters.Add("IdVis", id_vis);

                    client.UploadValuesAsync(uri, parameters);
                    client.UploadValuesCompleted += Client_UploadValuesCompleted;

                    
                }
                else
                {
                    Toast.MakeText(this, "Insira um ficheiro de voz", ToastLength.Long).Show();
                }
            };
        }

        private void MLinearLayout_Click(object sender, EventArgs e)
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

        protected override void OnResume()
        {
            base.OnResume();

            _recorder = new MediaRecorder();
            
        }

        protected override void OnPause()
        {
            base.OnPause();
            _recorder.Release();
            _recorder.Dispose();
            _recorder = null;
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                Toast.MakeText(this, "Grava��o inserida com sucesso", ToastLength.Long).Show();
                voice_data = null;
                _desc_voz.Text = "";
            });
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}