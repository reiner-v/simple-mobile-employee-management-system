<?php

include_once('connect.php');

    $pos = $_GET["emppos"];
	$s = mysqli_query($con,"SELECT samount FROM salary WHERE sposition='$pos'");


  if (mysqli_num_rows($s)==0)
    {
        echo "no rows";
    }
   else if (mysqli_num_rows($s)!=0)
    {
        $row = mysqli_fetch_assoc($s);
        echo $row["samount"];
    }
	mysqli_close($con);
?>