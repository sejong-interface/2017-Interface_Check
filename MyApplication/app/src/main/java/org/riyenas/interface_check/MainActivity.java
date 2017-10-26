package org.riyenas.interface_check;

import android.content.Intent;
import android.graphics.Color;
import android.net.Uri;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.view.Gravity;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.Button;
import android.widget.TextSwitcher;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ViewSwitcher.ViewFactory;

public class MainActivity extends ActionBarActivity{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void Inform(View v){
        Intent Act = new Intent(getApplicationContext(), Inform.class);
        startActivity(Act);
    }

    public void Absence(View v){
        Toast.makeText(getApplicationContext(), "건의사항이 제출되었습니다.", Toast.LENGTH_LONG).show();
    }

    public void Checkmem(View v){
        Toast.makeText(getApplicationContext(), "출석되었습니다", Toast.LENGTH_LONG).show();
    }

    public void Interfacemem(View v) {
        Intent Act = new Intent(getApplicationContext(), interfacemem.class);
        startActivity(Act);
    }

    public void control(View v) {
        Intent Act = new Intent(getApplicationContext(), LoginActivity.class);
        startActivity(Act);
    }

    public void GoogleDrive(View v){
        Toast.makeText(getApplicationContext(), "아이디 : interface518@gmail.com \n비밀번호 : interface518", Toast.LENGTH_LONG).show();
        Intent myIntent = new Intent(Intent.ACTION_VIEW, Uri.parse("https://www.google.com/intl/ko_ALL/drive/"));
        startActivity(myIntent);
    }
}
