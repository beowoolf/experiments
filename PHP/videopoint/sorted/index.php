<?php

function cmp_two_certs($a, $b) {
    $compare_dates_results = strcmp($a->d, $b->d);
    return ($compare_dates_results != 0 ? $compare_dates_results : strcmp($a->c, $b->c));
    /*if ($compare_dates_results != 0)
        return $compare_dates_results;
    return strcmp($a->c, $b->c);*/
}

$json = file_get_contents(__DIR__."/../videopoint.json");
$certificates = json_decode($json);
usort($certificates, "cmp_two_certs");

header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
echo(json_encode($certificates));

?>
