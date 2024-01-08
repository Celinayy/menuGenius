<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Desk extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function reservation():BelongsToMany
    {
        return $this->belongsToMany(Reservation::class);
    }

    public function purchase():BelongsToMany
    {
        return $this->belongsToMany(Purchase::class);
    }

}
