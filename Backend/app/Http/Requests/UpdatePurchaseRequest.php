<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Validation\Rule;

class UpdatePurchaseRequest extends FormRequest
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
            'total_pay' => ['integer'],
            'status' => ['string', Rule::in(['ordered', 'cooked', 'served'])],
            'paid' => ['boolean'],
            'user_id' => ['exists:users,id'],
            'desk_id' => ['exists:desks,id'],

            'products' => ['array'],
            'products.*.id' => ['exists:product_purchase,id'],
            'products.*.quantity' => ['integer', 'min:1'],
        ];
    }
}
