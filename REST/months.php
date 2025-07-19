<?php

include_once('connect.php');

	$m = mysqli_query($con,"SELECT * FROM months");
    $allmonth = array();


  if (mysqli_num_rows($m)==0)
    {
        echo "no rows";
    }
    else if (mysqli_num_rows($m)!=0)
    {
        while($value = mysqli_fetch_assoc($m))
        {  
            $allmonth[] = $value;
        }
        
        echo json_encode($allmonth);
    }
	mysqli_close($con);
?>