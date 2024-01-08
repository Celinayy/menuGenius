<?php

namespace App\Policies;

use App\Models\Allergen;
use App\Models\User;
use Illuminate\Auth\Access\Response;

class AllergenPolicy
{
    /**
     * Determine whether the user can view any models.
     */
    public function viewAny(User $user): bool
    {
        //
    }

    /**
     * Determine whether the user can view the model.
     */
    public function view(User $user, Allergen $allergen): bool
    {
        //
    }

    /**
     * Determine whether the user can create models.
     */
    public function create(User $user): bool
    {
        return $user->admin;
    }

    /**
     * Determine whether the user can update the model.
     */
    public function update(User $user, Allergen $allergen): bool
    {
        return $user->admin;
    }

    /**
     * Determine whether the user can delete the model.
     */
    public function delete(User $user, Allergen $allergen): bool
    {
        return $user->admin;
    }

    /**
     * Determine whether the user can restore the model.
     */
    public function restore(User $user, Allergen $allergen): bool
    {
        return $user->admin;
    }

    /**
     * Determine whether the user can permanently delete the model.
     */
    public function forceDelete(User $user, Allergen $allergen): bool
    {
        return $user->admin;
    }
}
