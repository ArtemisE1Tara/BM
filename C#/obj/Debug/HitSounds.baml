   M S B A M L   `   `   `  ����G  CBMBF Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BMBF_ManagerBMBF_Manager  5   	       BMBF_Manager.HitSounds   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:BMBF_Manager     5   	   -   5          � 
ResizeMode$  CanMinimize=�5   	    � WindowStyle$
 None=�5	   	    � Title$$ BMBF Manager HitSound Changing��5   	   $	��396��60   $	��420��6=   ���	#FF282828.��5      � .+�5   
   �� $��Logo.png��6   $
��Left=�6"   $��25��6=   $	��Top=�6I   $��25��6a   $��10,2,0,0q�6l   �� 5   
   -   5        $��X��5   G   $
��Left=�6S   $��	378,4,0,0q�6n   $	��Top=�6�   $��31��6�   ���	#00000000���	#FFCBCBCB6�   �� 5   
   -   5        $��|��5   F   $
��Left=�6R   $��
345,-2,0,0q�6m   $	��Top=�6�   $��20��6�   ���	#00000000���	#FFCBCBCB J� RenderTransformOrigin$ 0.5,0.5&�6�   $��31��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 90Q�6&   T� 5      5      5      �� 5   
   -   5         ��Name$ Quest��5   �   $��lucida console)�6+    J� Opacity$	 0.8Q�6�   $
��Left=�6�   $��23��6�   $��
203,30,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $��Quest IP��6�   $	��Top=�6   $	��207��6  ���	#FF494949���	#FFD30000
�������6S  �� 5   
   -   5        $ txtbox��5   v   $��lucida console)�6:   $	 0.8Q�6V    �� 
IsReadOnly . $
��Left=�6�   $	��356��6�   $��	10,30,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��188��6�   ���	#FF494949���	#FFA6A6A6���	#FFD300006'  �� 5   
   $	 0.8Q�6   $
��Left=�6#   $��24��6>   $	��Top=�6J   $	��249��6b   $ 0.5,0.5&�6n   $%��BMBF Manager - Sound Installing��6�   	 �� FontSize$	 14(�6�   
 �� 
Foreground

 �   �$��40,4,0,0q�6�   �� 5   
   -   5        $��lucida console)�5   9   $	 0.8Q�6U   $��Install Sound��6s   $
��Left=�6�   $��203,315,0,0q�6�   $	��Top=�6�   $	��207��6�   $��33��6�   $ -0.858,0.891&�6�   ���	#FF005654���	#FFF3F3F3���	#FFB947476>  �� 5   
   -   5        $��lucida console)�5   9   $	 0.8Q�6U   $��Choose Sound��6r   $
��Left=�6�   $��
203,58,0,0q�6�   $	��Top=�6�   $	��207��6�   $��33��6�   $ -0.858,0.891&�6�   ���	#FF005654���	#FFF3F3F3���	#FFB947476;  �� 5   
   -   5        $ Sound��5   v   $��lucida console)�6:   $	 0.8Q�6V    . $
��Left=�6�   $	��100��6�   $��
203,96,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��206��6�   ���	#FF494949���	#FFA6A6A6���	#FFD300006'  �� 5   
   -	   5         ��Name$ GoodHitSound��5   i   $��lucida console)�6M   $��As HitSound��6   $
��Left=�6�   $��203,201,0,0q�6�   $	��Top=�6�   ���	#FF005654���	#FFB94747���	#FFF3F3F36  �� 5    
   -
   5        $ BadHitSounds��5    h   $��lucida console)�6L   $��As BadHitSound��6~   $
��Left=�6�   $��203,220,0,0q�6�   $	��Top=�6�   ���	#FF005654���	#FFB94747���	#FFF3F3F36  �� 5!   
   -   5        $ 	MenuMusic��5!   f   $��lucida console)�6J   $��As Menu Music��6y   $
��Left=�6�   $��203,239,0,0q�6�   $	��Top=�6�   ���	#FF005654���	#FFB94747���	#FFF3F3F36  �� 5"   
   -   5        $ MenuClickSound��5"   k   $��lucida console)�6O   $��As Menu Click��6�   $
��Left=�6�   $��203,258,0,0q�6�   $	��Top=�6�   ���	#FF005654���	#FFB94747���	#FFF3F3F36  �� 5#   
   -   5        $ 	FireWorks��5#   k   $��lucida console)�6O   $��As Highscore Music��6~   $
��Left=�6�   $��203,277,0,0q�6�   $	��Top=�6�   ���	#FF005654���	#FFB94747���	#FFF3F3F36  �� 5$   
   -   5        $ LevelCleared��5$   i   $��lucida console)�6M   $��As Levelcleared��6   $
��Left=�6�   $��203,296,0,0q�6�   $	��Top=�6�   ���	#FF005654���	#FFB94747���	#FFF3F3F36  �� 5%   
   -   5        $��lucida console)�5%   9   $	 0.8Q�6U   $��Change Sound to default��6q   $
��Left=�6�   $��203,353,0,0q�6�   $	��Top=�6�   $	��207��6�   $��33��6�   $ -0.858,0.891&�6�   ���	#FF005654���	#FFF3F3F3���	#FFB947476F  5'      5(      