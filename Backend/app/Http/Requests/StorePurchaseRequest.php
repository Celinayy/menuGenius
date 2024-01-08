<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StorePurchaseRequest extends FormRequest
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
            'date_time' => ['required', 'datetime'],
            'total_pay' => ['required', 'integer'],
            'satus' => ['required', 'string', Rule::in(['ordered', 'cooked', 'served'])],
            'paid' => ['required', 'booleand'],
        ];
    }
}