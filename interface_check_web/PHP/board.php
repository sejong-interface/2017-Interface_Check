﻿<?php  
error_reporting(E_ALL); 
ini_set('display_errors',1); 

$link=mysqli_connect("localhost","interface518","518interface","interface518"); 
if (!$link)  
{ 
   echo "MySQL 접속 에러 : ";
   echo mysqli_connect_error();
   exit();
}  


mysqli_set_charset($link,"utf8");  

//POST 값을 읽어온다.
$name=isset($_POST['name']) ? $_POST['name'] : '';  
$contents=isset($_POST['contents']) ? $_POST['contents'] : '';  
 

if ($contents !="" and $name !=""){   
  
    $sql="insert into board(name, contents) values('$name','$contents')";  
    $result=mysqli_query($link,$sql);  

    if($result){  
       echo "SQL문 처리 성공";  
    }  
    else{  
       echo "SQL문 처리중 에러 발생 : "; 
       echo mysqli_error($link);
    } 
 
} else {
    echo "데이터를 입력하세요 ";
}


mysqli_close($link);
?>

<?php

$android = strpos($_SERVER['HTTP_USER_AGENT'], "Android");

if (!$android){
?>

<html>
   <body>
   
      <form action="<?php $_PHP_SELF ?>" method="POST">
         글쓴이 : <input type = "text" name = "name" />
	 내용 : <input type = "text" name = "contents" />
         <input type = "submit" />
      </form>
   
   </body>
</html>
<?php
}
?>
