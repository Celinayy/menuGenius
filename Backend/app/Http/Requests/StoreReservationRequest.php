<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

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
            // TODO: maximum 3 órás foglalás kezelése
            'checkout_date' => ['required', 'date_format:Y-m-d H:i:s', 'after:checkin_date'],
            'comment' => ['string', 'nullable'],
        ];
    }
}
