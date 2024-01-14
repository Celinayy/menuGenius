<?php

namespace App\Http\Controllers;


use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use App\Models\User;


class UserController extends Controller
{
    public function getUserData()
    {

        $user = auth()->user();

        if($user)
        {
            return User::where('id', $user->id)->with(['purchases'])->get();;
        }
        else
        {
            return response()->json(['error' => 'Ismeretlen felhasználó!'], 401);
        }
    }
}
