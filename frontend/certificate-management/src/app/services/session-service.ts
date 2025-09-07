import { isPlatformBrowser } from '@angular/common';
import { inject, Injectable, PLATFORM_ID } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private readonly SESSION_ID_KEY = 'app_session_id';
  private sessionId: string;
  private isBrowser: boolean;

  constructor(){
    this.isBrowser = isPlatformBrowser(inject(PLATFORM_ID));
    const storedSessionId = localStorage.getItem(this.SESSION_ID_KEY);

    if (this.isBrowser) {
      const storedSessionId = localStorage.getItem(this.SESSION_ID_KEY);
      if (storedSessionId) {
        this.sessionId = storedSessionId;
      } else {
        this.sessionId = crypto.randomUUID();
        localStorage.setItem(this.SESSION_ID_KEY, this.sessionId);
      }
    } else {
      this.sessionId = crypto.randomUUID();
    }
  }

  public getSessionId(): string {
    return this.sessionId;
  }
}
