<?php

namespace App\Providers;

// use Illuminate\Support\Facades\Gate;
use Illuminate\Foundation\Support\Providers\AuthServiceProvider as ServiceProvider;

class AuthServiceProvider extends ServiceProvider
{
    /**
     * The model to policy mappings for the application.
     *
     * @var array<class-string, class-string>
     */
    protected $policies = [
        Allergen::class => AllergenPolicy::class,
        Category::class => CategoryPolicy::class,
        EventLog::class => EventLogPolicy::class,
        Image::class => ImagePolicy::class,
        Ingredient::class => IngredientPolicy::class,
        Product::class => ProductPolicy::class,
        Reservation::class => ReservationPolicy::class,

    ];

    /**
     * Register any authentication / authorization services.
     */
    public function boot(): void
    {
        //
    }
}
