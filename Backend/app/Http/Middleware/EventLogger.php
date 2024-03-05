<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Symfony\Component\HttpFoundation\Response;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Auth;

class EventLogger
{
    /**
     * Handle an incoming request.
     *
     * @param  \Closure(\Illuminate\Http\Request): (\Symfony\Component\HttpFoundation\Response)  $next
     */
    public function handle(Request $request, Closure $next): Response
    {
        $logRoute = $request->fullUrl();
        $requestBody = $request->except(['password', 'password_confirmation']);
        $requestHeader = $request->headers;

        DB::table('event_logs')->insert([
            'event_type' => $request->method(),
            'user_id' => Auth::check() ? Auth::id() : null,
            'route' => $logRoute,
            'body' => json_encode($requestBody),
            'date_time' => now()
        ]);
        return $next($request);
    }
}
