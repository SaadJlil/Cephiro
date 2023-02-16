import { SignUp, SignInWithPassword, SignInWithGoogle, SignOut } from './types/auth';


export const signUp: SignUp = async (email, password, supabase) => {
    return await supabase.auth.signUp({
        email: email,
        password: password,
    })
}


export const signInWithPassword: SignInWithPassword = async (email, password, supabase) => {
    return await supabase.auth.signInWithPassword({
        email: email,
        password: password,
    })
}


export const signInWithGoogle: SignInWithGoogle = async (supabase) => {
    return await supabase.auth.signInWithOAuth({
        provider: 'google',
    })
}
    

export const signOut: SignOut = async (supabase) => {
    return  await supabase.auth.signOut()
}
