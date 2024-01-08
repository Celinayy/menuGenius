<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\AllergenController;
use App\Http\Controllers\CategoryController;
use App\Http\Controllers\DeskController;
use App\Http\Controllers\EventLogController;
use App\Http\Controllers\ImageController;
use App\Http\Controllers\IngredientController;
use App\Http\Controllers\ProductController;
use App\Http\Controllers\PurchaseController;
use App\Http\Controllers\ReservationController;
use App\Http\Controllers\UserController;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});

Route::resource("allergen", AllergenController::class)->except(["edit", "create"]);
Route::resource("category", CategoryController::class)->except(["edit", "create"]);
Route::resource("desk", DeskController::class)->except(["edit", "create"]);
Route::resource("eventlog", EventLogController::class)->except(["edit", "create"]);
Route::resource("image", ImageController::class)->except(["edit", "create"]);
Route::resource("ingredient", IngredientController::class)->except(["edit", "create"]);
Route::resource("product", ProductController::class)->except(["edit", "create"]);
Route::resource("purchase", PurchaseController::class)->except(["edit", "create"]);
Route::resource("reservation", ReservationController::class)->except(["edit", "create"]);
Route::resource("user", UserController::class)->except(["edit", "create"]);


Route::resource("ingredient", ProductController::class)->except(["edit", "create"]);