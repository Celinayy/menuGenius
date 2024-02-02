<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;
use Illuminate\Database\Eloquent\SoftDeletes;

class Image extends Model
{
    use HasFactory;
    use SoftDeletes;
    public $timestamps = false;
    protected $dates = ['deleted_at'];

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
