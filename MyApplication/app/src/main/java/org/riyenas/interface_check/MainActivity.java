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
    private static Button next;
    private static TextSwitcher textswitcher;

    // String array to be shown on textSwitcher
    String textToShow[] = {"동방 청소 열심히!", "이번주 집회는 6시 입니다",
            "코딩 공부 열심히!", "시험기간에는 집회가 없습니다."};

    // Total length of the string array
    int messageCount = textToShow.length;
    // to keep current Index of text
    int currentIndex = -1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Call all the methods
        init();
        loadAnimations();
        setFactory();
        setListener();
    }

    void init() {
        next = (Button) findViewById(R.id.buttonNext);
        textswitcher = (TextSwitcher) findViewById(R.id.textSwitcher);

    }

    void loadAnimations() {

        // Declare the in and out animations and initialize them
        Animation in = AnimationUtils.loadAnimation(this,
                android.R.anim.slide_in_left);
        Animation out = AnimationUtils.loadAnimation(this,
                android.R.anim.slide_out_right);

        // set the animation type of textSwitcher
        textswitcher.setInAnimation(in);
        textswitcher.setOutAnimation(out);
    }

    // Click listener method for button
    void setListener() {

        // ClickListener for NEXT button
        // When clicked on Button TextSwitcher will switch between texts
        // The current Text will go OUT and next text will come in with
        // specified animation
        next.setOnClickListener(new View.OnClickListener() {

            public void onClick(View v) {

                currentIndex++;
                // If index reaches maximum reset it
                if (currentIndex == messageCount)
                    currentIndex = 0;

                // Set the textSwitcher text according to current index from
                // string array
                textswitcher.setText(textToShow[currentIndex]);
            }
        });
    }

    // Set Factory for the textSwitcher *Compulsory part
    void setFactory() {
        textswitcher.setFactory(new ViewFactory() {

            public View makeView() {

                // Create run time textView with some attributes like gravity,
                // color, etc.
                TextView myText = new TextView(MainActivity.this);
                myText.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL);
                myText.setTextSize(25);
                myText.setTextColor(Color.WHITE);
                return myText;
            }
        });

    }


    public void Checkmem(View v){
        Intent Act = new Intent(getApplicationContext(), Inform.class);
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
        Intent Act = new Intent(getApplicationContext(), LoginActivity.class);
        startActivity(Act);
    }

    public void GoogleDrive(View v){
        Toast.makeText(getApplicationContext(), "아이디 : interface518@gmail.com \n비밀번호 : interface518", Toast.LENGTH_LONG).show();
        Intent myIntent = new Intent(Intent.ACTION_VIEW, Uri.parse("https://www.google.com/intl/ko_ALL/drive/"));
        startActivity(myIntent);
    }
}
