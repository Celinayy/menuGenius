<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Product extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function category()
    {
        return $this->hasOne(Category::class);
    }

    public function image()
    {
        return $this->belongsTo(Image::class);
    }

    public function ingredients() {
        return $this->belongsToMany(Ingredient::class);
    }

    public function purchases()
    {
        return $this->belongsToMany(Ingredient::class);
    }

}
