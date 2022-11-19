import { writable } from "svelte/store";

export const errMsg = writable<string>("");
export const errTitle = writable<string>("");

