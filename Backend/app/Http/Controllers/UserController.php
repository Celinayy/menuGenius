<?php

namespace App\Http\Controllers;

use App\Models\User;
use App\Http\Requests\StoreUserRequest;
use App\Http\Requests\UpdateUserRequest;
use Illuminate\Support\Facades\Hash;


class UserController extends Controller
{
    public function index()
    {

        $user = auth()->user();

        if($user)
        {
            return User::with(['purchases', 'reservations'])->find($user->id);;
        }
        else
        {
            return response()->json(['error' => 'Ismeretlen felhasználó!'], 401);
        }
    }

        /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreUserRequest $request)
    {
        // $input = $request->validated();
        // $input['password'] = Hash::make($input['password']);

        // $user = User::create($input);

        // $data = [
        //     'token' => $user->createToken('Sanctom+Socialite')->plainTextToken,
        //     'user' => $user,
        // ];

        // return response()->json($data, 200);
    }

    /**
     * Display the specified resource.
     */
    public function show(User $user)
    {
        if (!$user) {
            return response()->json(['error' => 'Nincs ilyen felhasználó!'], 404);
        }
        return response()->json($user);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(User $user)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateUserRequest $request)
    {
        $user = User::find(auth()->user()->id);

        if (!$user) {
            return response()->json(['error' => 'Felhasználó nem található.'], 404);
        }

        $data = $request->json()->all();

        if (isset($data['password'])) {
            $currentPassword = $data['current_password'];
    
            if (!Hash::check($currentPassword, $user->password)) {
                return response()->json(['error' => 'A régi jelszó nem egyezik.'], 422);
            }
        }

        $user->update($data);

        return response()->json(['message' => 'Az adatok sikeresen frissítve lettek.']);

        // $data = $request->json()->all();
        // $user->update($data);
        // return response()->json(['message' => 'Az adatok sikeresen frissítve lettek.']);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy($id)
    {
        $authenticatedUser = auth()->user();
        \Log::info('Authenticated User ID: ' . optional($authenticatedUser)->id);
        \Log::info('User to Delete ID: ' . $id);
    
        if ($authenticatedUser && $authenticatedUser->id == $id) {
            User::destroy($id);
            return response()->json(['message' => 'A profil sikeresen törölve lett.']);
        } else {
            return response()->json(['error' => 'Nincs engedélyed a profil törléséhez.'], 403);
        }
    }
}
