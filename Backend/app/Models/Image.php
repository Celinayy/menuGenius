<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;


class Image extends Model
{
    use HasFactory;
    public $timestamps = false;

    public $hidden = ["img_data"];
    public $appends = ["data"];

    public function product():HasOne
    {
        return $this->hasOne(Product::class);
    }
    public function getDataAttribute() {
        return base64_encode($this -> img_data);
    }
}
