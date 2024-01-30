<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateUserRequest extends FormRequest
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
            'name' => ['min:3'],
            'email' => ['unique:users', 'email'],
            'password' => ['confirmed', 'min:6'],
            'phone' => ['regex:/^([0-9\s\-\+\(\)]*)$/', 'min:10'],
        ];
    }
}
