import { makeAutoObservable } from "mobx"
export default class CounterStore {
    title = "Counter Store";
    count = 42;
    events: string[] = [
        `Initial Count Is ${this.count}.`
    ]

    constructor() {
        makeAutoObservable(this);
    }

    increament = (amount = 1) => {
        this.count += amount;
        this.events.push(`Incremented by ${amount} - Count is now ${this.count}`);
    }

    decrement = (amount = 1) => {
        this.count -= amount;
        this.events.push(`Decremented by ${amount} - Count is now ${this.count}`);
    }

    get eventCount(){
        return this.events.length;
    }
}