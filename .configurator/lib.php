<?php 
    if (!isset($_POST['load'])) {
        $safe_rq = array();
        foreach ($_REQUEST as $key => $value) {
            preg_match('/[+-]?([0-9]*[.])?[0-9]+/', $value, $matches);
            $sk = filter_var($key, FILTER_SANITIZE_STRING);
            $sv = floatval ($matches[0]);
            $safe_rq[$sk] = $sv;
        }
        $json = json_encode($safe_rq);
        file_put_contents('custom.json', $json);
        http_response_code(200);
        echo $json;
        return;
    } else {
        $mode = $_POST['load'];
        $filename = $mode ==! 0  && file_exists('custom.json') ? 'custom.json' : 'default.json';
        $json = file_get_contents($filename);
        http_response_code(200);
        echo $json;
        return;
    }
  /*
class Config {
    public $gravity = 2.5;
    public $fuel = 6.0;
    public $thrust = 620.0;
    public $torque = 15.0;
    public $mass = 3.0;
    
    public Save($filename) {
      file_put_contents($filename . '.json', json_encode($this));
    }
    
    public static LoadJson($filename) {
        
    }
}

$cfg = Config::Load('default');*/
?>

