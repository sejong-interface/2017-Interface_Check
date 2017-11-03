package org.riyenas.interface_check;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Toast;

public class MainActivity extends ActionBarActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void Inform(View v){
        Intent Act = new Intent(getApplicationContext(), InformActivity.class);
        startActivity(Act);
    }

    public void Absence(View v){
        Toast.makeText(getApplicationContext(), "건의사항이 제출되었습니다.", Toast.LENGTH_LONG).show();
    }


    public void Interfacemem(View v) {
        Intent Act = new Intent(getApplicationContext(), interfacemem.class);
        startActivity(Act);
    }

    public void control(View v) {
        Intent Act = new Intent(getApplicationContext(), SettingActivity.class);
        startActivity(Act);
    }

    public void checkmem(View v) {
        Intent Act = new Intent(getApplicationContext(), SuccessActivity.class);
        startActivity(Act);
    }

    public void GoogleDrive(View v){
        Toast.makeText(getApplicationContext(), "아이디 : interface518@gmail.com \n비밀번호 : interface518", Toast.LENGTH_LONG).show();
        Intent myIntent = new Intent(Intent.ACTION_VIEW, Uri.parse("https://www.google.com/intl/ko_ALL/drive/"));
        startActivity(myIntent);
    }

}
