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
            // 'number_of_guests' => ['required', 'integer'],
            // 'checkin_date' => ['required', 'datetime', 'after_or_equal:today'],
            // 'checkout_date' => ['required', 'datetime'],
            // 'name' => ['required', 'string'],
            // 'phone' => ['required', 'string'],
            // 'desk_id' => ['required', 'exists:desks,id'],
            // 'user_id' => ['required', 'exists:users,id']
        ];
    }
}
