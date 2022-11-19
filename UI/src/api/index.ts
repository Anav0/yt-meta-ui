import type { Video } from "../models/video";
import axios from "axios";

const instance = axios.create({
  baseURL: API_URL,
});

export const api = {
  videos: {

    getByPage: async (page: number, howMany: number, phrase: string = "") => {
      if (phrase.trim() == "")
        return await instance.get(`/Video/${page}/${howMany}`);
      else
        return await instance.get(`/Video/${page}/${howMany}/${phrase}`);
    }, total: async () => {
      return await instance.get(`/Video/total`);
    },
  },
};
