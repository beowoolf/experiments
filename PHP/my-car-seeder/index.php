<?php

$curl = curl_init();

curl_setopt_array($curl, [
  CURLOPT_URL => "https://api.mojezapiski.pl/my-car/",
  CURLOPT_RETURNTRANSFER => true,
  CURLOPT_ENCODING => "",
  CURLOPT_MAXREDIRS => 10,
  CURLOPT_TIMEOUT => 30,
  CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
  CURLOPT_CUSTOMREQUEST => "GET",
  CURLOPT_POSTFIELDS => "",
  CURLOPT_SSL_VERIFYHOST => 0,
  CURLOPT_SSL_VERIFYPEER => 0,
]);

$response = curl_exec($curl);
$err = curl_error($curl);

curl_close($curl);

if ($err) {
  echo "cURL Error #:" . $err;
} else {
  //echo $response;
  $obj = json_decode($response, false);
  $last_counter_value = $obj->counter;
}

?>

<!DOCTYPE html>
<html lang="pl">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>My car - seeder</title>
</head>
<body>
    Last counter value: <?=$last_counter_value; ?><br/>
    <?php
$journeys = array();
$lines = file('dane.csv');
foreach ($lines as $i => $line)
    if (strlen($line)>0) {
        $arr = explode(";", trim($line));
        $start = $arr[0];
        $end = $arr[1];
        $dist = $arr[2];
        $who = $arr[3];
        if ($start === "") $start = $end - $dist;
        if ($end === "") $end = $start + $dist;
        if ($dist === "") $dist = $end - $start;
        if ($last_counter_value <= $start) {
            $valid = ($last_counter_value == $start);
            $last_counter_value = $end;
            $journeys[] = array(
                "start" => $start,
                "end" => $end,
                "dist" => $dist,
                "who" => $who,
                "valid" => $valid ? 'green' : 'red'
            );
        }
    }
    ?>
    <!--<pre><?php print_r($journeys); ?></pre>-->
    <?php
    if (isset($_POST['password']) && $_POST['password']) {
        // echo("Password: ".$_POST['password']."<br/>");
        echo("<table>");
        echo("<thead>");
        echo("<th>Before</th>");
        echo("<th>After</th>");
        echo("<th>Distance</th>");
        echo("<th>Driver</th>");
        echo("<th>Result</th>");
        echo("</thead>");
        echo("<tbody>");
        foreach ($journeys as $i => $r) {
            $obj = array(
                "driver" => $r["who"],
                "before" => $r["start"],
                "after" => $r["end"]
            );

            $json = json_encode($obj);

            $curl = curl_init();
            
            curl_setopt_array($curl, [
              CURLOPT_URL => "https://api.mojezapiski.pl/my-car/",
              CURLOPT_RETURNTRANSFER => true,
              CURLOPT_ENCODING => "",
              CURLOPT_MAXREDIRS => 10,
              CURLOPT_TIMEOUT => 30,
              CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
              CURLOPT_CUSTOMREQUEST => "POST",
              CURLOPT_POSTFIELDS => $json,
              CURLOPT_HTTPHEADER => [
                "API-Key-Password: {$_POST['password']}",
                "Content-Type: application/json"
              ],
              CURLOPT_SSL_VERIFYHOST => 0,
              CURLOPT_SSL_VERIFYPEER => 0,
            ]);
            
            $response = curl_exec($curl);
            $err = curl_error($curl);
            
            curl_close($curl);
            
            if ($err) {
              echo "cURL Error #:" . $err;
            } else {
              //echo $response;
              $resp = json_decode($response, false);
              $result = ($resp->msg !== "Journey added :)" ? "Error" : "Saved");
              echo("<tr style='background-color:{$r['valid']}'><td>{$r['start']}</td><td>{$r['end']}</td><td>{$r['dist']}</td><td>{$r['who']}</td><td>$result</td></tr>");
              if ($result == "Saved")
                unset($journeys[$i]);
              else
                break;
            }
        }
        echo("</tbody>");
        echo("</table>");
        echo("<hr/>");
    }
    ?>
    <table>
        <thead>
            <th>Before</th>
            <th>After</th>
            <th>Distance</th>
            <th>Driver</th>
        </thead>
        <tbody>
            <?php
            foreach ($journeys as $i => $r)
                echo("<tr style='background-color:{$r['valid']}'><td>{$r['start']}</td><td>{$r['end']}</td><td>{$r['dist']}</td><td>{$r['who']}</td></tr>");
            ?>
        <tbody>
    </table>
    <form method="POST">
        API key password:<br />
        <input name="password" type="password" placeholder="enter API key password" /><br />
        <button type="submit">Send journeys</button>
    </form>
</body>
</html>
