import { animate, animateChild, group, query, style, transition, trigger } from "@angular/animations";

export const slideInAnimation = trigger('routeAnimations', [
    transition((from, to) => to === "horizontal" || from === "horizontal", [
        style({ position: 'relative' }),
        query(':enter, :leave', [
            style({
                position: 'absolute',
                top: 0,
                left: 0,
                width: '100%'
            })
        ], { optional: true }),
        query(':enter', [
            style({ left: '-100%' })
        ], { optional: true }),
        query(':leave', animateChild(), { optional: true }),
        group([
            query(':leave', [
                animate('200ms ease-out', style({ left: '-100%', opacity: 0 }))
            ], { optional: true }),
            query(':enter', [
                animate('300ms ease-out', style({ left: '0%' }))
            ], { optional: true }),
            query('@*', animateChild(), { optional: true })
        ]),
    ]),
    transition((from, to) => to === "vertical" || from === "vertical", [
        style({ position: 'relative' }),
        query(':enter, :leave', [
            style({
                position: 'absolute',
                top: 0,
                left: 0,
                width: '100%'
            })
        ], { optional: true }),
        query(':enter', [
            style({ transform: "translateY(100%)" })
        ], { optional: true }),
        query(':leave', animateChild(), { optional: true }),
        group([
            query(':leave', [
                animate('200ms ease-out', style({ transform: "translateY(100%)", opacity: 0 }))
            ], { optional: true }),
            query(':enter', [
                animate('300ms ease-out', style({ transform: "translateY(0)" }))
            ], { optional: true }),
            query('@*', animateChild(), { optional: true })
        ]),
    ]),
]);
