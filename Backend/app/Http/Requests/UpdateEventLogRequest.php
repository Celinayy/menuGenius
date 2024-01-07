<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateEventLogRequest extends FormRequest
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
            'eventType' => ['required', 'string', 'max:50'],
            'affectedTable' => ['required', 'string', 'max:50'],
            'affected_id' => ['required', 'integer'],
            'event' => ['required', 'string'],
            'date' => ['required', 'datetime']
        ];
    }
}
