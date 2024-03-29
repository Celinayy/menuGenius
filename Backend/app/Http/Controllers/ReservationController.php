<?php

namespace App\Http\Controllers;

use App\Models\Reservation;
use App\Http\Requests\StoreReservationRequest;
use App\Http\Requests\UpdateReservationRequest;
use \Illuminate\Database\Eloquent\ModelNotFoundException;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Carbon;
use Illuminate\Http\Request;
use App\Models\Desk;
use Mockery\Undefined;

class ReservationController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $user = Auth::user();

        if ($user->admin == true) {
            $reservations = Reservation::with(['desk', 'user'])->get();
            return $reservations;
        }
        if ($user->admin == false) {
            return Reservation::where('user_id', $user->id)->with(['desk'])->get();;
        } else {
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
        $checkinDateTime = Carbon::parse($request->input('checkin_date'));
        $checkoutDateTime = Carbon::parse($request->input('checkout_date'));

        $desk = $this->findFreeDesk(
            $request->input('number_of_guests'),
            $checkinDateTime,
            $checkoutDateTime,
        );

        if(!$desk) {
            return response()->json([
                'message' => 'A megadott igényekkel nincs szabad asztal!'
            ], 400);
        }

        if (Auth::guard('sanctum')->check()) {
            $user = Auth::guard('sanctum')->user();
        } else {
            $user = null;
        }

        $reservation = Reservation::create([
            'number_of_guests' => $request->input('number_of_guests'),
            'checkin_date' => $checkinDateTime,
            'checkout_date' => $checkoutDateTime,
            'name' => $request->input('name'),
            'phone' => $request->input('phone'),
            'comment' => $request->input('comment'),
            'desk_id' => $desk->id,
            'closed' => false,
            'user_id' => $user ? $user->id : null,
        ]);

        return response()->json(['message' => 'A foglalás létrehozva!', 'data' => $reservation], 201);
    }

    /**
     * Display the specified resource.
     */
    public function show($id)
    {
        if (auth()->check()) {
            $user = auth()->user();

            if ($user->admin) {
                $reservation = Reservation::with(['user', 'desk'])->find($id);

                if ($reservation) {
                    return response()->json($reservation, 200);
                } else {
                    return response()->json(['error' => 'Nincs ilyen vásárlás!'], 404);
                }
            } else {
                $reservation = $user->purchases()->with(['user', 'desk'])->find($id);

                if ($reservation) {
                    return response()->json($reservation, 200);
                } else {
                    return response()->json(['error' => 'Nincs jogosultságod ehhez a vásárláshoz.'], 403);
                }
            }
        } else {
            return response()->json(['error' => 'Ismeretlen felhasználó!'], 401);
        }
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Reservation $reservation)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateReservationRequest $request, $id)
    {
        $user = auth()->user();

        if (!$user) {
            return response()->json(['error' => 'Ismeretlen felhasználó!'], 401);
        }

        try {
            $reservation = Reservation::findOrFail($id);
        } catch (ModelNotFoundException $exception) {
            return response()->json(['error' => 'A foglalás nem található.'], 404);
        }

        $minimumCheckinDate = Carbon::now()->addHours(12);

        if ($user->admin || $user->id === $reservation->user_id) {
            if ($reservation->checkin_date > $minimumCheckinDate && $reservation->closed == false) {
                $reservation->update($request->only(['number_of_guests', 'checkin_date', 'checkout_date', 'name', 'phone', 'desk_id', 'user_id']));

                return response()->json($reservation->load(['user', 'desk']), 200);
            } else {
                return response()->json(['error' => 'A foglalás nem módosítható a foglalt időpont előtti 12 órán belül.'], 403);
            }
        }
        return response()->json(['error' => 'Nincs jogosultság a módosításra.'], 403);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy($id)
    {
        $user = auth()->user();
        $reservation = Reservation::find($id);

        if (!$reservation) {
            return response()->json(['error' => 'A foglalás nem található.'], 404);
        }

        if ($user->admin || $user->id != $reservation->user_id) {
            return response()->json(['error' => 'Nincs jogosultsága törölni ezt a foglalást.'], 403);
        }

        $reservation->delete();

        return response()->json(['message' => 'A foglalás sikeresen törölve.']);
    }

    private function findFreeDesk($number_of_guests, $check_in_date, $check_out_date)
    {
        $availableTable = Desk::where('number_of_seats', '>=', $number_of_guests)
            ->whereDoesntHave('reservation', function ($query) use ($check_in_date, $check_out_date) {
                $query->where('closed', '=', 0)
                    ->where(function ($query) use ($check_in_date, $check_out_date) {
                        $query->whereBetween('checkin_date', [$check_in_date, $check_out_date])
                            ->orWhereBetween('checkout_date', [$check_in_date, $check_out_date])
                            ->orWhere(function ($query) use ($check_in_date, $check_out_date) {
                                $query->where('checkout_date', '>=', $check_in_date);
                            });
                    });
            })
            ->orderBy('number_of_seats')
            ->first();

        return $availableTable;
    }

    public function checkAvailableDesk(Request $request)
    {
        $now = now();
        $request->validate([
            'checkin_date' => 'required|date_format:Y-m-d H:i:s',
            'checkout_date' => 'required|date_format:Y-m-d H:i:s|after:checkin_date',
            'number_of_guests' => 'required|integer|min:1',
        ]);

        $checkinDateTime = Carbon::parse($request->input('checkin_date'));
        $checkoutDateTime = Carbon::parse($request->input('checkout_date'));
        $number_of_guests = $request->input('number_of_guests');

        if ($checkinDateTime < $now) {
            return response()->json(['error' => 'A foglalás időpontja a jelenlegi időpontnál korábbi.'], 400);
        }

        $availableTable = $this->findFreeDesk($number_of_guests, $checkinDateTime, $checkoutDateTime);

        if ($availableTable) {
            return response()->json(['message' => 'Elérhető asztal.', 'desk' => $availableTable], 200);
        } else {
            return response()->json(['error' => 'Ebben az időpontban nem áll rendelkezésre asztal ennyi főre.'], 404);
        }
    }
}
