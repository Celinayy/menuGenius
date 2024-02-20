<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Support\Carbon;


class StoreReservationRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     */
    public function authorize(): bool
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array<mixed>|string>
     */
    public function rules(): array
    {
        return [
            'name' => ['required', 'string'],
            'phone' => ['required', 'string'],
            'number_of_guests' => ['required', 'integer', 'min:1'],
            'checkin_date' => ['required', 'date_format:Y-m-d H:i:s', 'after_or_equal:today'],
            'checkout_date' => ['required', 'date_format:Y-m-d H:i:s', 'after:checkin_date',
            function ($attribute, $value, $fail) {
                $checkin = Carbon::parse(request()->input('checkin_date'));
                $checkout = Carbon::parse($value);
                $maxCheckout = $checkin->copy()->addHours(3);
        
                if ($checkout->gt($maxCheckout)) {
                    $fail('3 óránál hosszabb időre nem lehet asztalt foglalni.');
                }
            },
        ],
            'comment' => ['string', 'nullable'],
        ];
    }
}
