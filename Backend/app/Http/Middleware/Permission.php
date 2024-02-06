<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Symfony\Component\HttpFoundation\Response;

class Permission
{
    /**
     * Handle an incoming request.
     *
     * @param  \Closure(\Illuminate\Http\Request): (\Symfony\Component\HttpFoundation\Response)  $next
     */
    public function handle(Request $request, Closure $next, $role): Response
    {
        if ($request->user() && $request->user()->admin == true &&
            ($role == "admin" ||
             $role == "user" ||
             $role == "guest")) {
            return $next($request);
        }

        if ($request->user() && $request->user()->admin == false &&
            ($role == "user" ||
            $role == "guest")) {
            return $next($request);
        }

        if ($request->user() && $request->user()->admin == false && $request->user()->name == "guest" &&
            ($role == "guest")) {
            return $next($request);
        }

        return response()->json(['error' => 'Nincs jogosultságod!'], 403);
    }
}
