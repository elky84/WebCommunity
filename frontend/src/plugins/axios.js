/* eslint-disable */
"use strict";

import Vue from 'vue'

import axios from "axios"
import router from '../router'

import axiosCookieJarSupport from 'axios-cookiejar-support'

import { CookieJar } from 'tough-cookie'

// Full config:  https://github.com/axios/axios#request-config
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
// axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

let config = {
  // baseURL: process.env.baseURL || process.env.apiUrl || ""
  timeout: 60 * 1000, // Timeout
};

const _axios = axios.create(config);
axiosCookieJarSupport(_axios);
_axios.defaults.jar = new CookieJar();
_axios.defaults.withCredentials = true

_axios.interceptors.request.use(
  function(config) {
    // Do something before request is sent
    return config;
  },
  function(error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

// Add a response interceptor
_axios.interceptors.response.use(
  function(response) {
    // Do something with response data
    return response;
  },
  function(error) {
    const vm = new Vue()
    // Do something with response error
    if (error.response) {
      if(error.response.status === 401) {
        console.log(error.response.config)
        vm.$bvModal.msgBoxConfirm('로그인이 필요합니다. 로그인 창으로 가시겠습니까?', {
          title:'Information'
        }).then(value => {
          router.push('/Account?mid=SignIn')
        })
      }
      else {
        vm.$bvModal.msgBoxOk(error.response.data.ErrorMessage, {
          title:'Server Error'
        })
      }
    }
    else {
      vm.$bvModal.msgBoxOk('서버에서 응답이 없습니다', {
        title:'Network Error'
      })
      console.log(error)
    }
    return Promise.reject(error);
  }
);

Plugin.install = function(Vue, options) {
  Vue.axios = _axios;
  window.axios = _axios;
  Object.defineProperties(Vue.prototype, {
    axios: {
      get() {
        return _axios;
      }
    },
    $axios: {
      get() {
        return _axios;
      }
    },
  });
};

Vue.use(Plugin)

export default Plugin;
