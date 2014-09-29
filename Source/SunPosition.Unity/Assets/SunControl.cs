using System;
using System.Globalization;
using System.Text;
using SunPosition;
using UnityEngine;

public class SunControl : MonoBehaviour {


    public double longitude;
    public double latitude;
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minut;
    public int second;


    private float lenght = 1500;
    private int time;
    private DateTime dateTime;

    // Use this for initialization
	void Start ()
	{
	    time = second + 60*(minut + 60*hour);

        dateTime = new DateTime(year, month, day, hour, minut, second);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    double height;
	    double azimuth;

        transform.position = new Vector3(0, 0, 2000);
        
        SunPosition.SunPosition.GetSunPosition(latitude, longitude, dateTime, out height, out azimuth);

        Debug.Log( new StringBuilder().AppendFormat("Height: {0}, azimuth {1}", height, azimuth).ToString());

	    var radius = lenght*Mathf.Cos((float) height.ToRadians());

        transform.position = new Vector3(radius * Mathf.Sin((float) azimuth.ToRadians()), lenght*Mathf.Sin((float) height.ToRadians()), radius*Mathf.Cos((float) azimuth.ToRadians()));
	}


    void OnGUI()
    {
        time = (int) GUI.HorizontalSlider(new Rect(10, 10, 600, 20), time, 0, 24*60*60);

        var timeSpan = TimeSpan.FromSeconds(time);

        dateTime = new DateTime(year, month, day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        
        GUI.TextArea(new Rect(10, 40, 200, 30), dateTime.ToString(CultureInfo.InvariantCulture));
    }
}
