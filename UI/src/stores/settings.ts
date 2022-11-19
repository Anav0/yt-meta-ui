import { writable } from "svelte/store";

export const page = writable<number>(1);
export const howMany = writable<number>(40);

