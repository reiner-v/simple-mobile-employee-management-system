<?php

include_once('connect.php');

	$places = mysqli_query($con,"SELECT * FROM branch");
    $allbranches = array();


  if (mysqli_num_rows($places)==0)
    {
        echo "no rows";
    }
    else if (mysqli_num_rows($places)!=0)
    {
        while($value = mysqli_fetch_assoc($places))
        {
            $allbranches[] = $value;
        }
        
        #echo implode(" ",$allbranches) error with array|array|array Warning array into string conversion;
        echo json_encode($allbranches);
    }
	mysqli_close($con);
?>