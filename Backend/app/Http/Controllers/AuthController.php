<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Auth;

class AuthController extends Controller
{
    public function register(Request $request)
    {
        $validator = Validator::make(
            $request->only('name', 'email', 'phone', 'password', 'password_confirmation'),
            [
                'name' => ['required', 'min:2', 'max:50', 'string'],
                'phone' => ['required', 'min:2', 'max:50', 'string'],
                'email' => ['required', 'email', 'unique:users,email'],
                'password' => ['required', 'min:6', 'max:255', 'confirmed', 'string'],

            ],
            [
                "name.required" => "A név megadása kötelező!",
                "name.min" => "Túl rövid név!",
                "name.max" => "Túl hosszú név!",

                "phone.required" => "A telefonszám megadása kötelező!",
                "phone.min" => "Túl rövid telefonszám!",
                "phone.max" => "Túl hossszú telefonszám!",

                "email.required" => "Az email megadása kötelező!",
                "email.email" => "Az email formátuma nem megfelelő!",
                "email.unique" => "Az email már foglalt!",

                "password.required" => "A jelszó kitöltése kötelező!",
                "password.min" => "A jelszó túl rövid!",
                "password.max" => "A jelszó túl hosszú!",
                "password.confirmed" => "A két jelszó nem egyezik meg!",
            ]
        );

        if ($validator->fails())
            return response()->json($validator->errors(), 400);

        $input = $request->only('name', 'email', 'password', "phone");
        $input['password'] = Hash::make($request['password']);
        $user = User::create($input);
        $data =  [
            'token' => $user->createToken('Sanctom+Socialite')->plainTextToken,
            'user' => $user,
        ];
        return response()->json($data, 200);
    }

    public function login(Request $request)
    {
        $validator = Validator::make($request->only('email', 'password'), [
            'email' => ['required', 'email', 'exists:users,email'],
            'password' => ['required', 'min:6', 'max:255', 'string'],
        ], [
            "email.required" => "Az email megadása kötelező!",
            "email.email" => "Az email formátuma nem megfelelő!",
            "email.exists" => "Az emailhez nem tartozik felhasználó!",

            "password.required" => "A jelszó mező kitöltése kötelező!",
            "password.min" => "A jelszónak minimum 6 karakternek kell lennie!",
            "password.max" => "A jelszó maximum 255 karakter lehet!",
        ]);

        if ($validator->fails())
            return response()->json($validator->errors(), 400);

        if (Auth::attempt(['email' => $request->email, 'password' => $request->password])) {
            $user = $request->user();
            $data =  [
                'token' => $user->createToken('Sanctom+Socialite')->plainTextToken,
                'user' => $user,
            ];
            return response()->json($data, 200);
        }
    }

    public function logout(Request $request)
    {
        $user = $request->user();

        if ($user) {
            $user->tokens()->where('id', $user->currentAccessToken()->id)->delete();
        }
        return response()->json(['message' => 'Sikeres kijelentkezés!']);
    }
}
