import { SupabaseClient } from '@supabase/auth-helpers-react';
import { AuthError, AuthResponse, OAuthResponse } from '@supabase/gotrue-js';

export declare type SignUp = (
  email: string,
  password: string,
  supabase: SupabaseClient<any, "public", any>
) => Promise<AuthResponse>;

export declare type SignInWithPassword = (
  email: string,
  password: string,
  supabase: SupabaseClient<any, "public", any>
) => Promise<AuthResponse>;

export declare type SignInWithGoogle = (
  supabase: SupabaseClient<any, "public", any>
) => Promise<OAuthResponse>;

export declare type SignOut = (
  supabase: SupabaseClient<any, "public", any>
) => Promise<{ error: AuthError | null; }>;