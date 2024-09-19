<?php

$php = file_get_contents("php.json");
$java = file_get_contents("java.json");

$php_obj = json_decode($php, true);
$java_obj = json_decode($java, true);

$php_urls = array();
$java_urls = array();

foreach ($php_obj as $key => $value)
    $php_urls[] = $value["url"];

foreach ($java_obj as $key => $value)
    if (isset($value["response"]))
        $java_urls[] = $value["response"]["url"];

$intersect = array_intersect($php_urls, $java_urls);
$u_php_urls = array_diff($php_urls, $intersect);
$u_java_urls = array_diff($java_urls, $intersect);
$all = array_merge($u_php_urls, $u_java_urls, $intersect);

echo(
    json_encode(
        array(
            "php" => $php_urls,
            "java" => $java_urls,
            "intersect" => $intersect,
            "u_php_urls" => $u_php_urls,
            "u_java_urls" => $u_java_urls,
            "all" => $all
        ),
        JSON_UNESCAPED_SLASHES
    )
);
