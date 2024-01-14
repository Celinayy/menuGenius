<?php

namespace App\Http\Controllers;

use App\Models\Reservation;
use App\Http\Requests\StoreReservationRequest;
use App\Http\Requests\UpdateReservationRequest;
use Illuminate\Support\Facades\Auth;


class ReservationController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $user = Auth::user();

        if($user)
        {
            return Reservation::where('user_id', $user->id)->with(['desk'])->get();;
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
    public function store(StoreReservationRequest $request)
    {
        $reservation = new Reservation();
        $reservation->number_of_guests = $request->input('number_of_guests');
        $reservation->checkin_date = $request->input('checkin_date');
        $reservation->checkout_date = $request->input('checkout_date');
        $reservation->name = $request->input('name');
        $reservation->phone = $request->input('phone');
        $reservation->desk_id = $request->input('desk_id');

        $reservation->save();

        return response()->json(['message' => 'A foglalás létrehozva!', 'data' => $reservation], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show(Reservation $reservation)
    {
        if (!$reservation) {
            return response()->json(['error' => 'Nincs ilyen foglalás!'], 404);
        }
    
        return response()->json($reservation);

        //$this->authorize('show-reservation', $reservation);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Reservation $reservation)
    {
        //$this->authorize('update-reservation', $reservation);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateReservationRequest $request, Reservation $reservation)
    {
        //$this->authorize('update-reservation', $reservation);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Reservation $reservation)
    {
        //$this->authorize('delete-reservation', $reservation);
        //$reservation->delete();    }
    }
}
