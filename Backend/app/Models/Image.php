<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Image extends Model
{
    use HasFactory;
    public $table = "image";
    public $timestamps = false;

    public function product():BelongsTo
    {
        return $this->belongsTo(Product::class);
    }
}
