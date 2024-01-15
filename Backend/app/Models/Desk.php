<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;


class Desk extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function reservation():HasMany
    {
        return $this->hasMany(Reservation::class);
    }

    public function purchase():HasMany
    {
        return $this->hasMany(Purchase::class);
    }

}
