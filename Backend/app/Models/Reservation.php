<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\BelongsTo;


class Reservation extends Model
{
    use HasFactory;
    public $timestamps = false;

    public function desk():BelongsTo
    {
        return $this->belongsTo(Desk::class);
    }

    public function user():BelongsTo
    {
        return $this->belongsTo(User::class)->whereNotNull('user_id');
    }
}
