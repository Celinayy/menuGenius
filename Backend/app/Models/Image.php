<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Image extends Model
{
    use HasFactory;
    public $timestamps = false;

    public $hidden = ["img_data"];
    public $appends = ["data"];

    public function product()
    {
        return $this->belongsTo(Product::class);
    }
    public function getDataAttribute() {
        return base64_encode($this -> img_data);
    }
}
