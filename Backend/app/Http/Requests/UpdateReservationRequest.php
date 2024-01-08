<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateReservationRequest extends FormRequest
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
            'numberOfGuests' => ['required', 'integer'],
            'checkInDate' => ['required', 'datetime', 'after_or_equal:today'],
            'checkOutDate' => ['required', 'datetime'],
            'name' => ['required', 'string'],
            'phone' => ['required', 'string'],
            'desk_id' => ['required', 'exsist:desks,id']
        ];
    }
}
