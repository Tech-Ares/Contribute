<?php
$data = "this is data";
echo $data."\n";
// import your private key
$privateKeyId = openssl_pkey_get_private(file_get_contents('../../priv/private.pem'));
// sign date with your private key
openssl_sign($data, $signature, $privateKeyId, 'RSA-SHA256');
// encode into base64
$ds_sign = base64_encode($signature);
// you may free up memory after, but I wouldn't recommend, since you are going to make many requests and sign each of them.
// importing key from file each time isn't brightest idea
openssl_free_key($privateKeyId);

echo "signature: \n" . $ds_sign . "\n";

// importing public key
$ds_pub_key = openssl_pkey_get_public(file_get_contents('../../priv/public.pem'));
// verifying signature for $data and imported public key
// note that signature firstly was decoded from base64
$valid = openssl_verify($data, base64_decode($ds_sign), $ds_pub_key, 'RSA-SHA256');
if ($valid == 1){
  echo "signature is valid \n";
} else {
  echo "signature is NOT valid\n";
}
// same thing about freeing of key
openssl_free_key($ds_pub_key);
?>
