package org.riyenas.interface_check;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;

import android.os.Bundle;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.util.Log;

import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;

import java.io.InputStream;
import java.io.InputStreamReader;

import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;
import com.melnykov.fab.FloatingActionButton;

import android.view.View;


public class BoardActivity extends AppCompatActivity {
    
    private static String TAG = "phptest";

    private static final String TAG_JSON="webnautes";
    private static final String TAG_NAME = "name";
    private static final String TAG_CONTENTS = "contents";
    private static final String TAG_TODAY ="today";


    private FloatingActionButton fab;
    private TextView mTextViewResult;
    ArrayList<HashMap<String, String>> mArrayList;
    ListView mlistView;
    String mJsonString;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_board);

        mTextViewResult = (TextView)findViewById(R.id.textView_result1);
        mlistView = (ListView) findViewById(R.id.listView_list1);
        mArrayList = new ArrayList<>();

        GetData task = new GetData();
        task.execute("http://interface518.dothome.co.kr/inter/getboard.php");


        fab = (FloatingActionButton) findViewById(R.id.fab);

        // 이벤트 적용
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent Act = new Intent(getApplicationContext(), BoardWriteActivity.class);
                startActivity(Act);
            }
        });
    }


    private class GetData extends AsyncTask<String, Void, String>{
        ProgressDialog progressDialog;
        String errorString = null;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();

            progressDialog = ProgressDialog.show(BoardActivity.this,
                    "Please Wait", null, true, true);
        }


        @Override
        protected void onPostExecute(String result) {
            super.onPostExecute(result);

            progressDialog.dismiss();
            mTextViewResult.setText(result);
            Log.d(TAG, "response  - " + result);

            if (result == null){

                mTextViewResult.setText(errorString);
            }
            else {

                mJsonString = result;
                showResult();
            }
        }


        @Override
        protected String doInBackground(String... params) {

            String serverURL = params[0];


            try {

                URL url = new URL(serverURL);
                HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();


                httpURLConnection.setReadTimeout(5000);
                httpURLConnection.setConnectTimeout(5000);
                httpURLConnection.connect();


                int responseStatusCode = httpURLConnection.getResponseCode();
                Log.d(TAG, "response code - " + responseStatusCode);

                InputStream inputStream;
                if(responseStatusCode == HttpURLConnection.HTTP_OK) {
                    inputStream = httpURLConnection.getInputStream();
                }
                else{
                    inputStream = httpURLConnection.getErrorStream();
                }


                InputStreamReader inputStreamReader = new InputStreamReader(inputStream, "UTF-8");
                BufferedReader bufferedReader = new BufferedReader(inputStreamReader);

                StringBuilder sb = new StringBuilder();
                String line;

                while((line = bufferedReader.readLine()) != null){
                    sb.append(line);
                }


                bufferedReader.close();


                return sb.toString().trim();


            } catch (Exception e) {

                Log.d(TAG, "InsertData: Error ", e);
                errorString = e.toString();

                return null;
            }

        }
    }


    private void showResult(){
        try {
            JSONObject jsonObject = new JSONObject(mJsonString);
            JSONArray jsonArray = jsonObject.getJSONArray(TAG_JSON);

            for(int i=0;i<jsonArray.length();i++){

                JSONObject item = jsonArray.getJSONObject(i);

                String name = item.getString(TAG_NAME);
                String contents = item.getString(TAG_CONTENTS);
                String today = item.getString(TAG_TODAY);

                HashMap<String,String> hashMap = new HashMap<>();

                hashMap.put(TAG_NAME, name);
                hashMap.put(TAG_CONTENTS, contents);
                hashMap.put(TAG_TODAY, today);

                mArrayList.add(hashMap);
            }

            ListAdapter adapter = new SimpleAdapter(
                    BoardActivity.this, mArrayList, R.layout.item_list2,
                    new String[]{TAG_NAME,TAG_CONTENTS, TAG_TODAY},
                    new int[]{R.id.textView_name_contents1, R.id.textView_contents_contents1, R.id.textView_today_contents1}
            );

            mlistView.setAdapter(adapter);

        } catch (JSONException e) {

            Log.d(TAG, "showResult : ", e);
        }

    }
}
