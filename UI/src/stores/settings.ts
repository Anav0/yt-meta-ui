import { writable } from "svelte/store";

export const page = writable<number>(1);
export const howMany = writable<number>(40);
export const order = writable<string>("none");
export const column = writable<string>("");