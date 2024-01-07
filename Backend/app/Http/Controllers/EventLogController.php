<?php

namespace App\Http\Controllers;

use App\Models\EventLog;
use App\Http\Requests\StoreEventLogRequest;
use App\Http\Requests\UpdateEventLogRequest;

class EventLogController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        $this->authorize('create', EventLog::class);
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreEventLogRequest $request)
    {
        $this->authorize('store', EventLog::class);
    }

    /**
     * Display the specified resource.
     */
    public function show(EventLog $eventLog)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(EventLog $eventLog)
    {
        $this->authorize('edit', EventLog::class);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateEventLogRequest $request, EventLog $eventLog)
    {
        $this->authorize('update', EventLog::class);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(EventLog $eventLog)
    {
        $this->authorize('destroy', EventLog::class);
    }
}
