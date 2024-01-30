<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;


class Reservation extends Model
{
    use HasFactory;
    public $timestamps = false;

    protected $fillable = [
        'number_of_guests',
        'checkin_date',
        'checkout_date',
        'name',
        'phone',
        'user_id',
        'desk_id',
        'closed',
        'comment'
    ];


    public function desk():BelongsTo
    {
        return $this->belongsTo(Desk::class);
    }

    public function user():BelongsTo
    {
        return $this->belongsTo(User::class)->whereNotNull('id');
    }
}
