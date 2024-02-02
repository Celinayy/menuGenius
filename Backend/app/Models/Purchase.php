<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Support\Facades\DB;
use Illuminate\Database\Eloquent\SoftDeletes;

class Purchase extends Model
{
    use HasFactory;
    use SoftDeletes;

    protected $fillable = [
        'date_time',
        'total_pay',
        'status',
        'paid',
        'user_id',
        'desk_id',
        'product_id'
    ];
    public $timestamps = false;
    protected $dates = ['deleted_at'];

    public function desk():BelongsTo
    {
        return $this->belongsTo(Desk::class);
    }

    public function user():BelongsTo
    {
        return $this->belongsTo(User::class)->whereNotNull('id');
    }

    public function products():BelongsToMany
    {
        return $this->belongsToMany(Product::class, 'product_purchase')->as('product_purchase')->withPivot('quantity');
    }

    public function recalculateTotalPay()
    {
        $totalPay = $this->products()->sum(DB::raw('price * quantity'));

        $this->total_pay = $totalPay;
        $this->save();
    }
}
